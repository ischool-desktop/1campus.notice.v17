

$(function () {
    $('#content').redactor({

        //imageUpload: '/upload.php',
        //imageManagerJson: '/images/images.json',
        buttons: ['formatting', 'bold', 'italic'],
        plugins: ['video', 'oimage']


    });
});


function tempFunction() {   
    var s = '.js呼叫成功';
    return s;
}


var setData = function (data) {
    var elm = document.getElementById("target");
    elm.innerHTML = data;
    return "success";
}

var getData = function () {
    //return $('#content').val();
    var elm = document.getElementById("content");

    return elm.value;
};

//test("");
//tempFunction();
