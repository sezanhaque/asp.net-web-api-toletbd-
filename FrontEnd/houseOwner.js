$(function(){

	var user = JSON.parse(sessionStorage.user);
	var id = user.Id;
	$('#userName').html(user.Name);


	//total pending post
	$.ajax({
		url:'http://localhost:2532/api/users/'+id+'/posts/totalPendingPost',
		headers:{
			Authorization:'Basic '+btoa(user.Email+':'+user.Password)
		},
		complete: function(xmlhttp,status){
			if(xmlhttp.status==200){
				$('#totalPendingPost').html(xmlhttp.responseText);
				//alert(xmlhttp.responseText);
			}
		}
	});


	//total published post
	$.ajax({
		url:'http://localhost:2532/api/users/'+id+'/posts/totalPublishedPost',
		headers:{
			Authorization:'Basic '+btoa(user.Email+':'+user.Password)
		},
		complete: function(xmlhttp,status){
			if(xmlhttp.status==200){
				$('#totalPendingPost').html(xmlhttp.responseText);
				//alert(xmlhttp.responseText);
			}
		}
	});

	

	//for logout

	$('#logout').click(function(){
		$.ajax({
			url:'http://localhost:2532/api/users/logout',
			headers:{
				Authorization:'Basic '+btoa(user.Email+':'+user.Password)
			},
			complete: function(xmlhttp,status){
				
					sessionStorage.removeItem('user')
					window.location.href = "login.html";

				
			}
		});

	});



	// for profile page
	$('#Name').val(user.Name);
	$('#Email').val(user.Email);
	$('#PhnNo').val(user.PhnNo);
	$('#Password').val(user.Password);


	$('#profileForm').submit(function(event) {
     
     	var id = user.Id;
		 $.ajax({ 
        	url: 'http://localhost:2532/api/users/'+id,
        	type: 'PUT',
            headers:{
				Authorization:'Basic '+btoa(user.Email+':'+user.Password)
			},
			data:{
            	Name:$('#Name').val(),
            	Email: $('#Email').val(),
            	Password:$('#Password').val(),
            	PhnNo:$('#PhnNo').val()
            
			},
            complete: function(xmlhttp, status) {
            	if(xmlhttp.status==200){
            		var a = JSON.parse(xmlhttp.responseText);

					sessionStorage.setItem('user', JSON.stringify(a));
					var obj = JSON.parse(sessionStorage.user);
					alert('Profile Updated!!');
					window.location.href = "houseOwnerProfile.html";

            	}else{
            		alert(xmlhttp.status+': Not Updated!!');

            	}
                
            }
        });
        return false; 
    });


    // for current Posts

    $.ajax({ 
      url: 'http://localhost:2532/api/users/'+id+'/posts',
      type: 'GET',
        headers:{
    Authorization:'Basic '+btoa(user.Email+':'+user.Password)
  },
        complete: function(xmlhttp, status) {
          if(xmlhttp.status==200){
            
            var postList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<postList.length;i++){

						if(postList[i].Status=='Pending'){
							outputStr+='<div class="panel panel-success"><div class="panel-heading"><strong>'+postList[i].Title+'</strong></div><div class="panel-body"><div class="col-md-12"><div class="row"><div class="col-md-6"> <div class="row"><div class="col-md-6"><p style="width:100%;"><strong>No of Room: '+postList[i].NoOfRoom+'</strong></p></div><div class="col-md-6"><p style="width:100%;"><strong>House Rent: '+postList[i].RoomRent+' tk</strong></p></div></div></div><div class="col-md-6"><textarea style="width:100%;">'+postList[i].ShortDesc+'</textarea></div></div></div><div class="col-md-12"><div class="row"><hr /><div class="col-md-12"><div class="col-md-8"><strong>Address: '+postList[i].Address+'</strong></div><div class="col-md-4"><strong>Status: '+postList[i].Status+'</strong></div></div><div class="col-md-12"><hr /><div class="col-md-offset-3 col-md-6"><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-danger deletePostBtn">Delete</button></a></div><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-success publishBtn">Publish</button></a></div></div></div></div></div></div></div>';

						}else{
							outputStr+='<div class="panel panel-success"><div class="panel-heading"><strong>'+postList[i].Title+'</strong></div><div class="panel-body"><div class="col-md-12"><div class="row"><div class="col-md-6"> <div class="row"><div class="col-md-6"><p style="width:100%;"><strong>No of Room: '+postList[i].NoOfRoom+'</strong></p></div><div class="col-md-6"><p style="width:100%;"><strong>House Rent: '+postList[i].RoomRent+' tk</strong></p></div></div></div><div class="col-md-6"><textarea style="width:100%;">'+postList[i].ShortDesc+'</textarea></div></div></div><div class="col-md-12"><div class="row"><hr /><div class="col-md-12"><div class="col-md-8"><strong>Address: '+postList[i].Address+'</strong></div><div class="col-md-4"><strong>Status: '+postList[i].Status+'</strong></div></div><div class="col-md-12"><hr /><div class="col-md-offset-3 col-md-6"><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-danger deletePostBtn">Delete</button></a></div><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-warning disableBtn">Disable</button></a></div></div></div></div></div></div></div>';

						}
					}
					$('#currentPosts').html(outputStr);
            

          }
            
        }
    });


    // for new post 

    $('#NewPostForm').submit(function(event) {
     
     	var id = user.Id
     	var date = new Date();
		var formated = date.getFullYear() + "-" + (date.getMonth()+1) + "-" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
		
		 $.ajax({ 
        	url: 'http://localhost:2532/api/posts',
        	type: 'POST',
            headers:{
				Authorization:'Basic '+btoa(user.Email+':'+user.Password)
			},
			data:{

				PostDateTime: formated,
				RoomRent:$('#RoomRent').val(),
				NoOfRoom:$('#NoOfRoom').val(),
            	Title:$('#Title').val(),
            	Address: $('#Address').val(),
            	ShortDesc:$('#ShortDesc').val(),
            	Status:'Pending',
            	Views:0,
            	UserId:id
            
			},
            complete: function(xmlhttp, status) {
            	if(xmlhttp.status==200){
            		alert('Post Added Successfully!!');
					window.location.href = "houseOwnerCurrentPosts.html";

            	}else{
            		alert(xmlhttp.status+': Not Added!!');

            	}
                
            }
        });
        return false; 
    });



});