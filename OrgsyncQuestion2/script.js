$(document).ready(function() {
	$("#submitBtn").click(function() {
		var firstName = $.trim($('#nameInput').val().split(',')[1]);
		var lastName = $.trim($('#nameInput').val().split(',')[0]);
		var status = $('#statusInput').val();
		passInData(firstName, lastName, status);
	});
});

function passInData(firstN, lastN, status) {
	var xmlhttp;

	xmlhttp = new XMLHttpRequest();
	xmlhttp.open("POST", "/member/save", false);
	xmlhttp.send(lastN +"&" + firstN + "&" +status);
	
	
	$("#personTable").prepend($('<tr>').append($('<td>').append(lastN + ", " + firstN)).append($('<td>').append(status)));

	$("table").tablesorter();
	var sorting = [[0, 0]];
	$("table").trigger("update");
	$("table").trigger("sorton", [sorting]);
	return false;
}
