        npm install -g browserify
        npm install qrcode

write main.js

        var QRCode = require("qrcode");
        global.window.QRCode = QRCode;

        browserify main.js -o b1.js
        uglifyjs bundle.js -o b2.js
        copy b2.js src/bundle.js

sample stackoverflow page
https://stackoverflow.com/questions/4912092/using-html5-canvas-javascript-to-take-in-browser-screenshots
https://stackoverflow.com/questions/51215736/uwp-send-file-data-to-webview