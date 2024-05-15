window.saveAsFile = (fileName, content) => {

const blob = new Blob([content]); //Oppretter objekt som vil evt være dataen jeg vil lagre 
const url = URL.createObjectURL(blob); //brukes for å referere dataen jeg ønsker å laste ned
const link = document.createElement('a'); //oppretter link element som tar initiativ til nedlastning 

link.href = url;
link.download = fileName;

document.body.appendChild(link);//linken blir lagt til kroppen
link.click(); //ved klikk startes nedlastning
document.body.removeChild(link); //etter klikk, så vil linken løse fra seg
URL.revokeObjectURL(url); //frigjører minne etter nedlastning 

};


