const http = require('node:http');
const fs = require('node:fs');
const path = require('node:path');
const files = new Map([
  ['/', ['index.html', 'text/html; charset=utf-8']],
  ['/desktop.html', ['desktop.html', 'text/html; charset=utf-8']],
  ['/mobile.html', ['mobile.html', 'text/html; charset=utf-8']]
]);
http.createServer((req, res) => {
  const match = files.get(new URL(req.url, 'http://127.0.0.1').pathname);
  if (!match) { res.writeHead(404); res.end(); return; }
  fs.readFile(path.join(__dirname, match[0]), (err, content) => {
    if (err) { res.writeHead(404); res.end('Taslak hazırlanıyor.'); return; }
    res.writeHead(200, {'Content-Type':match[1], 'Cache-Control':'no-store'});
    res.end(content);
  });
}).listen(4174, '127.0.0.1', () => console.log('Tasarım inceleme: http://127.0.0.1:4174'));
