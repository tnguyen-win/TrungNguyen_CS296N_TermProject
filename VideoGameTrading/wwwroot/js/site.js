// jshint esversion: 6

const checkPageLine = () => {
	const eBtn = document.getElementById('buttonScrollToTop');
	let classes = [];

	classes = document.body.scrollTop > 20 || document.documentElement.scrollTop > 20 ? ['invisible', 'visible'] : ['visible', 'invisible'];

	eBtn.classList.replace(classes[0], classes[1]);
}

const scrollToTop = () => {
	document.body.scroll({
		top: 0,
		left: 0,
		behavior: 'smooth'
	});
	document.documentElement.scroll({
		top: 0,
		left: 0,
		behavior: 'smooth'
	});
}

window.onload = checkPageLine;
window.onscroll = checkPageLine;
