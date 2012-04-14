define( ["backbone", "jquery", "hogan", "underscore"], function( backbone, $, hogan, _ ) {
  person = backbone.Model.extend( {
    initialize: function() {
      this.bind( 'error', 'handleError' );
    },
    handleError: function( model, error ) {
      $( error ).each( function( index, elem ) {
        console.log( elem );
      } );
    },
    validate: function( attrs ) {
      var er = [];
      if ( !attrs.name ) {
        //console.log('Name is invalid');
        er.push( 'Name:' + attrs.Name + '<- Name is invalid ' );
      }

      if ( attrs.age < 18 ) {
        //console.log('you are under age');
        er.push( 'Age:' + attrs.age + '<- you are under age' );
      }

      if ( !attrs.address ) {
        //
        er.push( 'Address:' + attrs.address + '<- please insert a correct address' );
      }

      if ( er.length > 0 ) {

        return er;
      }


    }
  } );
  personCollection = backbone.Collection.extend( {
    mode: person
  } );
  personView = backbone.View.extend( {
    initialize: function() {
      this.collection = new personCollection();
      this.collection.bind( 'add', this.addperson );
      this.mode = new person();
      this.render();
    },
    addperson: function( model ) {
      $( 'ul' ).append( '<li>' + model.get( 'name' ) + ',' + model.get( 'age' ) + '</li>' );
    },
    render: function() {
      this.template = _.template( $( '#gameView' ).html() );
      $( this.el ).html( this.template );
      this.model = new person();
      return this;
    },
    events: {
      'click button[type=submit]': 'savePerson',
      'click button[type=cancel]': 'cancelEdit',
      'change input': 'inputdata'

    },
    inputdata: function( ev ) {
      var val = $( ev.currentTarget ).val();
      var attr = ev.currentTarget.id;
      console.log( attr, ',', val );
      this.model.set( { attr: val } );
    },

    savePerson: function() {
      this.collection.add( new person( { name: $( '#name' ).val(), age: $( '#age' ).val(), address: $( '#address' ).val() } ) );
    },
    cancelEdit: function() {
      $( 'input' ).val( '' );
    }

  } );
  var personview = new personView( { el: $( '#gameView' ), model: new person() } );
  personview.render();
} );
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

