window.saveAsFile = (filNavn, filInnhold) => {

    var link = document.createElement('a'); //oppretter a-elemnt (html). link
    link.download = filNavn; //setter download -> filnavn
    link.href = blobUrl; //setter href -> bloburl
    document.body.appendChild(link); //a-elementet blir lagt til i kroppen
    link.click(); //nedlastning skjer ved klikk
    document.body.removeChild(link); 

};

//Kilder: https://wellsb.com/csharp/aspnet/blazor-jsinterop-save-file
// Gir muligheten for å kunne eksportere.
// Referert i App.razor