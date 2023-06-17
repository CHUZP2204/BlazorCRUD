function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

//Esta funcion recibe  el nombre de un archivo y un archivo en base 64 y lo que hace es crear un
//link dinamico y hace que se descargue un archivo