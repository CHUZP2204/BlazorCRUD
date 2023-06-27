
//Se realiza este codigo para comunicarse con blazor codigo C# para actualizar la hora cada segundo
//se recibia un objeto de tipo date enviado desde C# pero no coincidia con el JS, por lo que se
//Realiza este codigo para convertir el dato recibido en tipo DATE compatible con JavaScript y no presentar
//Errores de datos nulos o que funciones no existen en el contexto recibido

window.updateCurrentTime = (time) => {

    //Obtener elemento con el ID = current-time desde pagina html
    
    const currentTimeElement = document.getElementById('current-time');

    //verificamos si existe el elemento que se busco en el paso anterior

    if (currentTimeElement) {

        //Declara una variable que almacenara el formato de nuestra hora
        let formattedTime;

        //Preguntamos si el objeto recibido en variable u objeto 'time'
        //Es de tipo DATE, si es asi se accede directamente a funciones de clase DATE
        //En caso contrario convertir objeto recibido a formato DATE
        if (time instanceof Date) {
            formattedTime = time.toLocaleTimeString();
        } else {
            //Se crea nueva instancia de DATE y se le asigna variable recibido 'time'
            const date = new Date(time);
            if (date instanceof Date && !isNaN(date)) {
                formattedTime = date.toLocaleTimeString();
            } else {
                formattedTime = '';
            }
        }
        currentTimeElement.innerText = formattedTime;
    }
    
};

