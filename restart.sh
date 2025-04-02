#!/bin/bash

echo "🔄 Restarting Phoenix-AmazingCharts EHR Application..."

# Kill specific processes using our ports
echo "🛑 Stopping any running processes..."

# Function to kill process using a specific port
kill_process_on_port() {
    local port=$1
    local pid=$(lsof -ti:$port)
    if [ -n "$pid" ]; then
        echo "   Killing process $pid on port $port"
        kill -9 $pid 2>/dev/null || true
        sleep 1
    fi
}

# Kill any processes using our ports
kill_process_on_port 3000  # Proxy server
kill_process_on_port 5002  # Blazor app

# Navigate to the project directory
cd "$(dirname "$0")"

# Start the proxy server in the background
echo "🚀 Starting proxy server..."
cd proxy
node server.js > proxy.log 2>&1 &
PROXY_PID=$!
cd ..

# Give the proxy server time to start
echo "⏳ Waiting for proxy server to initialize..."
sleep 3

# Check if proxy server started successfully
if ! lsof -i:3000 -t > /dev/null 2>&1; then
    echo "❌ Proxy server failed to start. Check proxy/proxy.log for details."
    cat proxy/proxy.log
    exit 1
fi

# Start the Blazor application
echo "🚀 Starting Blazor application..."
cd AmazingCharts
dotnet run --urls=http://localhost:5002 > ../blazor.log 2>&1 &
BLAZOR_PID=$!
cd ..

# Give the Blazor app time to start
echo "⏳ Waiting for Blazor application to initialize..."
sleep 5

# Check if Blazor app started successfully
if ! lsof -i:5002 -t > /dev/null 2>&1; then
    echo "❌ Blazor application failed to start. Check blazor.log for details."
    cat blazor.log
    exit 1
fi

echo "✅ Application started successfully!"
echo "📋 Process Information:"
echo "   - Proxy Server PID: $PROXY_PID"
echo "   - Blazor App PID: $BLAZOR_PID"
echo "🌐 Access the application at: http://localhost:5002"
echo "⚠️ Press Ctrl+C to stop all processes"

# Wait for user to press Ctrl+C
trap "echo '🛑 Stopping all processes...'; kill $PROXY_PID $BLAZOR_PID 2>/dev/null || true" INT TERM
wait $BLAZOR_PID
