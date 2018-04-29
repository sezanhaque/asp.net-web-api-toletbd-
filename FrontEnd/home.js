$(function(){



	// for all Post

    $.ajax({ 
      url: 'http://localhost:2532/api/posts/byStatus/Active',
      type: 'POST',
        complete: function(xmlhttp, status) {
          if(xmlhttp.status==200){
            
            var postList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<postList.length;i++){
						outputStr+='<div class="panel panel-success"><div class="panel-heading"><strong>'+postList[i].Title+'</strong></div><div class="panel-body"><div class="col-md-12"><div class="row"><div class="col-md-6"> <div class="row"><div class="col-md-6"><p style="width:100%;"><strong>No of Room: '+postList[i].NoOfRoom+'</strong></p></div><div class="col-md-6"><p style="width:100%;"><strong>House Rent: '+postList[i].RoomRent+' tk</strong></p></div></div></div><div class="col-md-6"><textarea style="width:100%;">'+postList[i].ShortDesc+'</textarea></div></div></div><div class="col-md-12"><div class="row"><hr /><div class="col-md-12"><div class="col-md-8"><strong>Address: '+postList[i].Address+'</strong></div><div class="col-md-4"><strong>Status: '+postList[i].Status+'</strong></div></div><div class="col-md-12"><hr /><div class="col-md-6"><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-success viewBtn">View Details</button></a></div></div></div></div></div></div></div>';
					}
					$('#homeAllPost').html(outputStr);
            

          }
            
        }
    });


    //for search post by area

    $('#searchPostBtn').click(function(){
    	$.ajax({ 
        	url: 'http://localhost:2532/api/posts/byArea/'+$('#PostAreaBox').val(),
        	type: 'POST',
            complete: function(xmlhttp, status) {
            	if(xmlhttp.status==200){
            		
					var postList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<postList.length;i++){
						outputStr+='<div class="panel panel-success"><div class="panel-heading"><strong>'+postList[i].Title+'</strong></div><div class="panel-body"><div class="col-md-12"><div class="row"><div class="col-md-6"> <div class="row"><div class="col-md-6"><p style="width:100%;"><strong>No of Room: '+postList[i].NoOfRoom+'</strong></p></div><div class="col-md-6"><p style="width:100%;"><strong>House Rent: '+postList[i].RoomRent+' tk</strong></p></div></div></div><div class="col-md-6"><textarea style="width:100%;">'+postList[i].ShortDesc+'</textarea></div></div></div><div class="col-md-12"><div class="row"><hr /><div class="col-md-12"><div class="col-md-8"><strong>Address: '+postList[i].Address+'</strong></div><div class="col-md-4"><strong>Status: '+postList[i].Status+'</strong></div></div><div class="col-md-12"><hr /><div class="col-md-6"><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-success viewBtn">View Details</button></a></div></div></div></div></div></div></div>';
					}
					$('#homeAllPost').html(outputStr);

            	}
                
            }
        });
    });



});