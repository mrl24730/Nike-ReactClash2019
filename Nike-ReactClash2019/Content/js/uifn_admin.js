$(document).ready(function(){
	$("form.ajax-form").submit(function(e) {
		var that = $(e.currentTarget);
	    $.ajax({
			type: that.prop("method"),
			url: that.prop("action"),
			data: that.serialize(), // serializes the form's elements.
			success: function(response)
			{
				responseData(response,that); // show response from the php script.
			}
		});
	    e.preventDefault(); // avoid to execute the actual submit of the form.
	   	return false;
	});

	function responseData(response,form){
		var handle = $(form).data("handle");

		switch(handle){
			case "login":
				form.find(".invalid-feedback").removeClass("d-block");
				if(response.Code === 0){
					location.href = $(form).data("redirect");
				}else{
					//Error
					form.find(".invalid-feedback").addClass("d-block");
				}
				break;
		}
	}	
});
