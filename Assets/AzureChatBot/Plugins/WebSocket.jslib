mergeInto(LibraryManager.library, {

  ConnectToWebSocket: function (str) {

    console.log("Connecting to websocket: " + Pointer_stringify(str));
	var connection = new WebSocket(Pointer_stringify(str));

	connection.onopen = function () {
	  console.log('Connected to websocket');
	};

	connection.onerror = function (error) {
	  console.log('WebSocket Error ' + error);
	};

	connection.onmessage = function (e) {
	  console.log('Server: ' + e.data);
	  SendMessage('DirectLineConnection', 'OnMessageReceived', e.data);
	};

  },

});