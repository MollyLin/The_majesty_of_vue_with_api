//Vue.component('story', {
//  template: "#template-story-raw",
//  props: ['story']
//  });
// 
//  var vm = new Vue({
//  el: '#app',
//  data: {
//  stories: []
//  },
//  mounted: function(){
////  $.get('/Vue/GetStoriesList', function(result){
////  vm.stories = result.Data;
////  })
//   var vm = this;
//         $.ajax({
//            type: 'post',
//            url: "GetStoriesList",
//            dataType: 'json',
//            beforeSend: function () { },
//            success: function (result, textStatus, jqXHR) {
//                console.log(result);

//                var getData = result.Data;

//                if (result.Success) {
//                    //  vm.stories = result.Data.StoriesList;
//                } else {
//                    // vm.errorMsg = result.Data.ErrMsg;
//                }
//            },
//            error: function () {

//            },
//            complete: function () { }
//        })
//          }
//  })