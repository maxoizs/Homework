require(["backbone", "jquery", "hogan", "underscore"], function (backbone, $, hogan, _) {

//    var xhr = new XMLHttpRequest();

//    xhr.onreadystatechange = function () {
//        if ((xhr.readyState == 4) && (xhr.status == 200)) {

//            var model = JSON.parse(xhr.responseText);

//            //  $("#testModal").html(hogan.compile($("#testModal").html().replace(/=\"\"/g, '/')).render(model));
//            $("#gameView").html(hogan.compile($("#gameView").html().replace(/=\"\"/g, '/')).render(model));
//        }
//    };
//    xhr.open("GET", "/default/persons", true);


//    xhr.send();
    var persone = backbone.Model.extend({
        intialize:function () {
            alert("hi");
        },
        defaults: {
            name:'has no name yet',
            age:0,
        },
        validate: function (attrs) {
            if (attrs.length > 0)
                return "you more than enough";
        }
    });
    var mo = new persone({ name: "Mohammad", age: 30 });
    $('#gameView').html(mo.get('age'));
    mo.set({ age: 40 });
    $('#gameView').html(mo.get('age'));
   
    });
   
    //return modal.render();

