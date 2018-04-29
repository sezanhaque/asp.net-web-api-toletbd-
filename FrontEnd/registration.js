$(function(){

	// for new post 

    $('#UserRegForm').submit(function(event) {
     
   
		 $.ajax({ 
        	url: 'http://localhost:2532/api/users',
        	type: 'POST',
			data:{

				Name:$('#Name').val(),
				Email:$('#Email').val(),
            	Password:$('#Password').val(),
            	PhnNo: $('#PhnNo').val(),
            	Gender:$('#Gender').val(),
            	UserTypeId:2
            
			},
            complete: function(xmlhttp, status) {
            	if(xmlhttp.status==200){
            		alert('Registration Success!!');
					window.location.href = "login.html";

            	}else{
            		alert(xmlhttp.status+': Something went wrong!!');

            	}
                
            }
        });
        return false; 
    });

});