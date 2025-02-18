function IsLogged()
{
	const user = sessionStorage.getItem('user');
	if (user == undefined)
		return false;
	const expirationDate = sessionStorage.getItem('expirationUtc');
	return true; // TODO verifica che il token non sia scaduto
}
function DisplayUser()
{
	const user = sessionStorage.getItem('user');
	if (user == undefined)
		return;
	const expirationDate = sessionStorage.getItem('expirationUtc');
	
	var now = new Date;
	var dateNow = Date.UTC(now.getUTCFullYear(),now.getUTCMonth(), now.getUTCDate() , 
		  now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds(), now.getUTCMilliseconds());
	
	let displayDate = '';
	console.log(expirationDate);
	console.log(dateNow);
	if (expirationDate > dateNow)
	{
		const delta = new Date(null);
		delta.setSeconds((expirationDate - dateNow) / 1000);
		displayDate = `Scade in ${delta.toISOString().slice(11, 19)}`;
	}
	else
	{
		displayDate = `<a href="./auth.html">Sessione scaduta!</a>`;
	}
	$('.container').append(`
		<div style="position: fixed; top: 0; right: 0">
			<p class="m-0">${user}</p>
			<p class="m-0">${displayDate}</p>
		</div>`);
}
async function DeletePost(postId)
{
	try
	{
		const response = await axios.delete(`https://localhost:7190/Posts/${postId}`); 
		$(`#Post_${postId}`).remove();
		console.log(response);
	}
	catch (err)
	{
		alert(err);
		console.log(err);
	}
}