define(["backbone", "jquery", "hogan", "underscore"], function (backbone, $, hogan, _) {
    Person = backbone.Model.extend({
        initialize: function () {
            console.log('Hello ' + this.get('name') + ' - Age:' + this.get('age'));

            this.bind('change', function (x) {
                console.log(x.get('name'), x.get('age'));

            });
            this.bind('error', function (model, error) {
                $(error).each(function (index, elem) {
                    console.log(elem);
                });

            });
        },
        validate: function (attrs) {
            var er = [];
            if (!attrs.name) {
                //console.log('Name is invalid');
                er.push('Name:' + attrs.Name + '<- Name is invalid ');
            }

            if (attrs.age < 18) {
                //console.log('you are under age');
                er.push('Age:' + attrs.age + '<- you are under age');
            }

            if (!attrs.address) {
                //
                er.push('Address:' + attrs.address + '<- please insert a correct address');
            }

            if (er.length > 0) {

                return er;
            }


        }
    });
    PersonView = backbone.View.extend({
        initialize: function () {
            this.render();
            console.log('Iam at the view ');
            //console.log(this.el.html());
        },
        render: function () {
            //console.log(_.template($('#gameView').html(), {}));
            this.template = _.template($('#gameView').html()); // $("#gameView");
            var x = this.template(this.model.toJSON());
            $(this.el).html(x);
            return this;
        },
        events: {
            'click button[type=submit]': 'savePerson',
            'click button[type=cancel]': 'cancelEdit'
        },
        savePerson: function () {
            console.log('save person');
        },
        cancelEdit: function () {

            console.log('clear form');
        }

    });
    var person = new Person({ name: 'Mohammad', age: '30' });
    var personview = new PersonView({ el: $('#gameView'), model: person });
    personview.render();
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
    //start of test Rout
    //   var MainRouter = backbone.Router.extend({
    //   
    //   Router:{
    //   "gameview" : "homepage",
    //   "index" : "indexpage",
    //   "/home/id":this.hompageID,
    //   },
    //   indexpage:function(){
    //       alert("here");
    //     $('#gameView').html("this is index");
 //   homepageID:function(id){alert("here");
    //    $('#gameView').html("your ID is "+id);
    //   },
    //   });
    //end of testing the Rout
    //James Test
    //    return = backbone.Model.extend({
    //        urlRoot : '/Default/Persons',
    //        intialize:function () {
    //            this.bind(All);
    //            this("change","attrChange");
    //            this.fetch();
    //        },
     //    });
    // End of James TEst
    //    var mo = new persone({ name: "Mohammad", age: 30 });
    //    var sta = new persone({});
    //    sta.fetch();
    ////    sta.save();
    //    $('#gameView').html(mo.get('age'));
    //return modal.render();

