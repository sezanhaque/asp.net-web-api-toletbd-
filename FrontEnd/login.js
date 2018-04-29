$(function(){

	$('#LoginBtn').click(function(){

		$.ajax({
			url: 'http://localhost:2532/api/users/login',
			method: 'PUT',
			header: 'Content-Type: application/json',
			data:{
				Email: $('#Email').val(),
				Password: $('#Password').val(),
			},
			complete: function(xmlhttp, status){
				if(xmlhttp.status==200){
					var a = JSON.parse(xmlhttp.responseText);

					sessionStorage.setItem('user', JSON.stringify(a));
					var obj = JSON.parse(sessionStorage.user);

					if(obj.UserTypeId==1){
						window.location.href = "admin.html";
					}else if(obj.UserTypeId==2){
						window.location.href = "houseOwner.html";
					}else{
						alert("You have no panel yet!!");
					}
					

				}else{
					alert("Invalid logins!!");
				}
			}
		});
	});
});