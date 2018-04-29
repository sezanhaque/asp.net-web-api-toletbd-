$(function(){



	var user = JSON.parse(sessionStorage.user);

	

	$('#userName').html(user.Name);

	//for total houseowner
	$.ajax({
		url:'http://localhost:2532/api/users/totalHouseOwner',
		headers:{
			Authorization:'Basic '+btoa(user.Email+':'+user.Password)
		},
		complete: function(xmlhttp,status){
			if(xmlhttp.status==200){
				$('#totalHouseOwner').html(xmlhttp.responseText);
				//alert(xmlhttp.responseText);
			}
		}
	});

	//total pending post
	$.ajax({
		url:'http://localhost:2532/api/posts/totalPendingPost',
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
		url:'http://localhost:2532/api/posts/totalPublishedPost',
		headers:{
			Authorization:'Basic '+btoa(user.Email+':'+user.Password)
		},
		complete: function(xmlhttp,status){
			if(xmlhttp.status==200){
				$('#totalPublishedPost').html(xmlhttp.responseText);
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
					window.location.href = "profile.html";

            	}else{
            		alert(xmlhttp.status+': Not Updated!!');

            	}
                
            }
        });
        return false; 
    });


    //for all users

    $.ajax({ 
        	url: 'http://localhost:2532/api/users',
        	type: 'GET',
            headers:{
				Authorization:'Basic '+btoa(user.Email+':'+user.Password)
			},
            complete: function(xmlhttp, status) {
            	if(xmlhttp.status==200){
            		
					var userList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<userList.length;i++){
						outputStr+='<div class="panel panel-default"><div class="panel-body"><div class="row"><div class="col-md-10 col-sm-12"><div  class="row"><div class="col-md-6"><div class="row"><div class="col-md-12"><p id="userId"><span style="font-size: 16px; font-weight: bold;">Id: </span>'+userList[i].Id+'</p></div><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">Name: </span>'+userList[i].Name+'</p></div><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">Email: </span>'+userList[i].Email+'</p></div></div></div><div class="col-md-4 col-sm-12"><div class="row"><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">Gender: </span>'+userList[i].Gender+'</p></div><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">User Type Id: '+userList[i].UserTypeId+'</span></p></div></div></div><div class="col-md-2 col-sm-12"><div class="row"><div class="col-md-12"><button type="button" value="'+userList[i].Id+'" onclik="deleteuser()" id="userDeleteButton" class="btn btn-danger">Delete</button></div></div></div></div></div></div></div></div>'
					}
					$('#userList').html(outputStr);

            	}
                
            }
        });


    //for search user by types

    $('#searchUserBtn').click(function(){
    	$.ajax({ 
        	url: 'http://localhost:2532/api/userTypes/'+$('#userSearchBox').val()+'/users',
        	type: 'GET',
            headers:{
				Authorization:'Basic '+btoa(user.Email+':'+user.Password)
			},
            complete: function(xmlhttp, status) {
            	if(xmlhttp.status==200){
            		
					var userList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<userList.length;i++){
						outputStr+='<div class="panel panel-default"><div class="panel-body"><div class="row"><div class="col-md-10 col-sm-12"><div  class="row"><div class="col-md-6"><div class="row"><div class="col-md-12"><p id="userId"><span style="font-size: 16px; font-weight: bold;">Id: </span>'+userList[i].Id+'</p></div><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">Name: </span>'+userList[i].Name+'</p></div><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">Email: </span>'+userList[i].Email+'</p></div></div></div><div class="col-md-4 col-sm-12"><div class="row"><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">Gender: </span>'+userList[i].Gender+'</p></div><div class="col-md-12"><p><span style="font-size: 16px; font-weight: bold;">User Type Id: '+userList[i].UserTypeId+'</span></p></div></div></div><div class="col-md-2 col-sm-12"><div class="row"><div class="col-md-12"><button type="button" value="'+userList[i].Id+'" id="userDeleteButton" class="btn btn-danger">Delete</button></div></div></div></div></div></div></div></div>'
					}
					$('#userList').html(outputStr);

            	}
                
            }
        });
    });

    // for allPending Post

    $.ajax({ 
      url: 'http://localhost:2532/api/posts/byStatus/Pending',
      type: 'POST',
        headers:{
    Authorization:'Basic '+btoa(user.Email+':'+user.Password)
  },
        complete: function(xmlhttp, status) {
          if(xmlhttp.status==200){
            
            var postList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<postList.length;i++){
						outputStr+='<div class="panel panel-success"><div class="panel-heading"><strong>'+postList[i].Title+'</strong></div><div class="panel-body"><div class="col-md-12"><div class="row"><div class="col-md-6"> <div class="row"><div class="col-md-6"><p style="width:100%;"><strong>No of Room: '+postList[i].NoOfRoom+'</strong></p></div><div class="col-md-6"><p style="width:100%;"><strong>House Rent: '+postList[i].RoomRent+' tk</strong></p></div></div></div><div class="col-md-6"><textarea style="width:100%;">'+postList[i].ShortDesc+'</textarea></div></div></div><div class="col-md-12"><div class="row"><hr /><div class="col-md-12"><div class="col-md-8"><strong>Address: '+postList[i].Address+'</strong></div><div class="col-md-4"><strong>Status: '+postList[i].Status+'</strong></div></div><div class="col-md-12"><hr /><div class="col-md-offset-3 col-md-6"><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-danger deletePostBtn">Delete</button></a></div><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-success publishBtn">Publish</button></a></div></div></div></div></div></div></div>';
					}
					$('#allpendingPosts').html(outputStr);
            

          }
            
        }
    });


    // for all published Post

    $.ajax({ 
      url: 'http://localhost:2532/api/posts/byStatus/Active',
      type: 'POST',
        complete: function(xmlhttp, status) {
          if(xmlhttp.status==200){
            
            var postList = xmlhttp.responseJSON;
					var outputStr='';
					for(var i=0;i<postList.length;i++){
						outputStr+='<div class="panel panel-success"><div class="panel-heading"><strong>'+postList[i].Title+'</strong></div><div class="panel-body"><div class="col-md-12"><div class="row"><div class="col-md-6"> <div class="row"><div class="col-md-6"><p style="width:100%;"><strong>No of Room: '+postList[i].NoOfRoom+'</strong></p></div><div class="col-md-6"><p style="width:100%;"><strong>House Rent: '+postList[i].RoomRent+' tk</strong></p></div></div></div><div class="col-md-6"><textarea style="width:100%;">'+postList[i].ShortDesc+'</textarea></div></div></div><div class="col-md-12"><div class="row"><hr /><div class="col-md-12"><div class="col-md-8"><strong>Address: '+postList[i].Address+'</strong></div><div class="col-md-4"><strong>Status: '+postList[i].Status+'</strong></div></div><div class="col-md-12"><hr /><div class="col-md-offset-3 col-md-6"><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-warning disableBtn">Disable</button></a></div><div class="col-md-4"><a style="text-decoration:none;"><button style="width:100%;" type="button" value="'+postList[i].Id+'" class="btn btn-danger deletePostBtn2">Delete</button></a></div></div></div></div></div></div></div>';
					}
					$('#allpublishedPosts').html(outputStr);
            

          }
            
        }
    });
    

    
	
});