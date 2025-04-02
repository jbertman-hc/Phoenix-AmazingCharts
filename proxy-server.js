const http = require('http');
const https = require('https');
const url = require('url');

const PORT = 3000;
const TARGET_HOST = 'apiserviceswin20250318.azurewebsites.net';

// Create a simple proxy server
const server = http.createServer((req, res) => {
  // Set CORS headers
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type, Authorization, X-Requested-With');
  res.setHeader('Access-Control-Max-Age', '86400'); // 24 hours
  
  // Handle preflight requests
  if (req.method === 'OPTIONS') {
    res.writeHead(200);
    res.end();
    return;
  }

  const parsedUrl = url.parse(req.url, true);
  
  // Only proxy requests starting with /api
  if (!parsedUrl.pathname.startsWith('/api')) {
    res.writeHead(404);
    res.end('Not Found');
    return;
  }

  console.log(`Proxying request: ${req.method} ${parsedUrl.pathname}`);
  
  // Include query parameters if any
  let targetPath = parsedUrl.pathname;
  if (parsedUrl.search) {
    targetPath += parsedUrl.search;
  }

  // Options for the proxy request
  const options = {
    hostname: TARGET_HOST,
    port: 443,
    path: targetPath,
    method: req.method,
    headers: {
      ...req.headers,
      host: TARGET_HOST
    }
  };

  // Remove headers that might cause issues
  delete options.headers['host'];
  delete options.headers['origin'];
  delete options.headers['referer'];

  console.log(`Forwarding to https://${TARGET_HOST}${targetPath}`);

  // Create the proxy request
  const proxyReq = https.request(options, (proxyRes) => {
    // Log the response status
    console.log(`Response from API: ${proxyRes.statusCode} ${proxyRes.statusMessage}`);
    
    // Copy all headers from the API response
    Object.keys(proxyRes.headers).forEach(key => {
      res.setHeader(key, proxyRes.headers[key]);
    });
    
    // Ensure CORS headers are set in the response
    res.setHeader('Access-Control-Allow-Origin', '*');
    
    // Set the status code
    res.writeHead(proxyRes.statusCode);
    
    // Pipe the response data
    proxyRes.pipe(res, { end: true });
  });

  // Handle errors
  proxyReq.on('error', (err) => {
    console.error('Proxy request error:', err);
    res.writeHead(500);
    res.end(JSON.stringify({ error: 'Proxy Error', message: err.message }));
  });

  // Add a timeout
  proxyReq.setTimeout(10000, () => {
    proxyReq.destroy();
    console.error('Proxy request timeout');
    res.writeHead(504);
    res.end(JSON.stringify({ error: 'Gateway Timeout', message: 'Request to API timed out' }));
  });

  // Pipe the request data to the proxy request
  req.pipe(proxyReq, { end: true });
});

// Start the server
server.listen(PORT, () => {
  console.log(`Proxy server running at http://localhost:${PORT}`);
  console.log(`Proxying requests to https://${TARGET_HOST}`);
});
