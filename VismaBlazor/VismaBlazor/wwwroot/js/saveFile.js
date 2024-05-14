window.saveAsFile = (filNavn, innhold) => {
    const blobObjekt = new Blob([innhold], { type: 'application/json' }); //Oppretter objekt som vil evt være dataen jeg vil lagre
    const url = URL.createObjectURL(blobObjekt); //brukes for å referere dataen jeg ønsker å laste ned
    const link = document.createElement('a'); //oppretter link element som tar initiativ til nedlastning 
    link.href = url;
    link.download = filNavn;
    document.body.appendChild(link);//linken blir lagt til kroppen
    link.click(); //ved klikk startes nedlastning
    document.body.removeChild(link); //etter klikk, så vil linken løse fra seg
    URL.revokeObjectURL(url);
};


