window.saveAsFile = (filNavn, innhold) => {
    const blobObjekt = new Blob([innhold], { type: 'application/json, text/csv, text/plain' }); //Oppretter konstant variabel
    const url = URL.createObjectURL(blobObjekt); //url genereres, brukes for å referere til dataer

    const link = document.createElement('a'); //oppretter link element som tar initiativ til nedlastning
    link.href = url;
    link.download = filNavn;
    document.body.appendChild(link); //linken blir lagt til kroppen
    link.click(); //ved klikk startes nedlastning
    document.body.removeChild(link); //etter klikk, så vil linken løse fra seg

    URL.revokeObjectURL(url); //for å holde nettleseren effektiv bruker vi denne for å frigjøre minne
};