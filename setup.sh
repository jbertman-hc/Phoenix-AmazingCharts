#!/bin/bash

echo "🔧 Setting up Phoenix-AmazingCharts EHR Application..."

# Navigate to the project directory
cd "$(dirname "$0")"

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo "❌ Node.js is not installed. Please install Node.js to run the proxy server."
    exit 1
fi

# Check if npm is installed
if ! command -v npm &> /dev/null; then
    echo "❌ npm is not installed. Please install npm to run the proxy server."
    exit 1
fi

# Install proxy server dependencies
echo "📦 Installing proxy server dependencies..."
cd proxy
npm install
cd ..

# Make restart script executable
echo "🔑 Making restart script executable..."
chmod +x restart.sh

echo "✅ Setup completed successfully!"
echo "🚀 To start the application, run: ./restart.sh"
