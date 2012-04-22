require(["backbone", "jquery", "hogan", "underscore"], function (backbone, $, hogan, _) {

    var Person = backbone.Model.extend({
        initialize:function () {
          _.bindAll(this);
          this.bind('error':'handleerror');
        },
        handleerror: function (model,error){
          console.log('this is handle error function');
        },
        defaults: {
            name:'',
            age:18,
            isMale:1,
            address:'',
        },
        validate: function (attrs) {
     
          var er = [];
          if (!attrs.name)
            er.push(["Name", "you must have a name dud"]);
          if (attrs.age < 18)
            er.push(["Age", "you are under age mate"]);
          if (attrs.address.length<5) {
            er.push(["Address", "so where do you live boss"]);
          }
          
          if (er.length>0) {
            return er;
          }
            
        }
        });
  var mo = new Person({ name: "Mohammad", age: 30 });
  mo.set({address:''});
  console.log(mo);
  mo.set({ name: '', age: 15, address: '' });
  console.log(mo);
  
  var PersonView = backbone.View.extend({
     el:$('#gameView'),
     initialize:function () {
      _.bindAll(this, 'render');
      this.render();
     console.log(el);
    },
   
    render:function () {
      this.el.html(_.template($('#gameView').html));
    },
    events: {
      "click button[.btn-primary]":"Save",
      "click button[.cancel]":"Cancel"

    },
    Save:function () {
      console.log('Save Now');
      
    },
    Cancel:function () {
      console.log('Cancel All');
      
    },    
  });
   

   //var view = new PersonView();
    });
   


   
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