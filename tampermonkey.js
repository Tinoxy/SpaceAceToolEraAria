// ==UserScript==
// @name         New Userscript
// @namespace    http://tampermonkey.net/
// @version      2024-03-30
// @description  try to take over the world!
// @author       You
// @match        https://space-aces.com/game?lang=cz
// @icon         https://www.google.com/s2/favicons?sz=64&domain=tampermonkey.net
// @grant        none
// ==/UserScript==

(function() {
    'use strict';
    // Array to store WebSocket connections
    window.SocketList = [];
    // Store reference to original WebSocket constructor
   var OriginalWS =window._0x1699652dewg;
    console.debug(OriginalWS);

    // Override WebSocket constructor
      window._0x1699652dewg = function(...args) {
        // Create new WebSocket instance
        var ws = new OriginalWS(...args);
        // Add WebSocket instance to SocketList
        window.SocketList.push(ws);
        // Intercept onopen event
        var originalOnOpen = ws.onopen;
        var websocket = null;
        var originalSend = null;
        var ws2 = new OriginalWS("ws://localhost:8999");

        // Override onopen event handler
        ws.onopen = function(event) {
            // Call original onopen handler if exists
            if (originalOnOpen) {
                originalOnOpen.call(this, event);
            }
            // Add custom functionality
            console.debug("WebSocket connection opened");
            // Check if URL contains "8443" and store WebSocket instance
            if (ws.url.includes("8443") ||ws.url.includes("2053")) {
                websocket = ws;
                originalSend = ws.send;
                  ws.send = function(data) {
              if(!originalSend)
              {
                  return;
              }
              ws2.send(data);
            // Add custom functionality to capture data
            console.debug("Data sent:", data);
            // Call original send method
            originalSend.call(this, data);
        };
            }
        };
        // Create new WebSocket connection to localhost:8999
        // Handle incoming messages on original WebSocket
        ws.onmessage = function(event) {
            // Forward message to ws2
            ws2.send(event.data);
        };
        // Handle incoming messages on ws2
        ws2.onmessage = function(event) {
            console.debug(event.data + " " + websocket);
            var messageSplit = event.data.split("&");
            if (messageSplit.length > 1 && websocket) {
                // Send message to original WebSocket
                if(messageSplit[0] == "send")
                {
                    websocket.send(messageSplit[1]);
                    return;
                }
                if(messageSplit[0] == "receive")
                {
                    websocket.dispatchEvent(new MessageEvent("message" , {data: messageSplit[1]}));

                }

            }
        };
        // Handle ws2 onopen event
        ws2.onopen = function(event) {
            console.debug("WebSocket connection to localhost:8999 opened");
        };
        // Handle errors
        ws.onerror = function(event) {
            console.error("WebSocket connection error:", event);
        };
        // Return WebSocket instance
        return ws;
    };
})();