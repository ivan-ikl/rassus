var NewQuestionsAvailable = false;

var AndacolClient = function () {
	var self = this;

	$.connection.hub.url = "http://andacol.azurewebsites.net/signalr";
	self.andacolHub = $.connection.andacolHub;

	self.andacolHub.client.questionsUpdated = function () {
	    NewQuestionsAvailable = true;
	};

	self.connect = function () {
		///<summary>Connects the SignalR hub</summary>
		$.connection.hub.start({ jsonp: true }).fail(function (error) {
            console.log("SignalR error", error);
        });
	};

	self.disconnected = function () {
		///<summary>SignalR hub has disconnected. Attempts to reconnect after 10 seconds</summary>
		setTimeout(self.connect, 10000);
        console.log("SignalR Disconnected!");
	};

	self.connect();
	$.connection.hub.disconnected(self.disconnected);
}