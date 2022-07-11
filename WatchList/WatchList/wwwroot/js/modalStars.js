function rate(elem) {
	var id = document.getElementById("TMDB").value;  
	var type = document.getElementById("media_type").value;  
	var rate_ = elem.value;

	if (type=="Movie")
		$.get("/Movie/Rate", { movieId: id, rate: rate_ });
	else
		$.get("/TV/Rate", { tvId: id, rate: rate_ });

}


