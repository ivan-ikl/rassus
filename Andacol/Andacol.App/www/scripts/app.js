// We use an "Immediate Function" to initialize the application to avoid leaving anything behind in the global scope
(function () {
    function renderHomeView() {
        var html =
          "<h1>Directory</h1>" +
          "<input class='search-key' type='search' placeholder='Enter name'/>" +
          "<ul class='employee-list'></ul>";
        $('body').html(html);
        $('.search-key').on('keyup', findByName);
    }

    /* ---------------------------------- Local Variables ---------------------------------- 
    var service = new EmployeeService();
    service.initialize().done(function () {
        console.log("Service initialized");
    });
    */
    var service = new EmployeeService();
    service.initialize().done(function () {
        renderHomeView();
    });
    /* --------------------------------- Event Registration -------------------------------- 
    $('.search-key').on('keyup', findByName);
    */
    $('.help-btn').on('click', function() {
        alert("Employee Directory v3.4");
    });
    document.addEventListener('deviceready', onDeviceReady, false);
    function onDeviceReady() {
        console.log(navigator.notification);
    }

    /* ---------------------------------- Local Functions ---------------------------------- 
    function findByName() {
        service.findByName($('.search-key').val()).done(function (employees) {
            var l = employees.length;
            var e;
            $('.employee-list').empty();
            for (var i = 0; i < l; i++) {
                e = employees[i];
                $('.employee-list').append('<li><a href="#employees/' + e.id + '">' + e.firstName + ' ' + e.lastName + '</a></li>');
            }
        });
    }
    */
}());