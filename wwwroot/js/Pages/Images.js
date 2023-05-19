async function ReturnGeneratedImage(data) {

    const response = await fetch('http://localhost:50864/ImageWebApi/Get?data=' + data.value + "&AImodel=text-davinci-003");
    const json = await response.json();
    var url = json.data[0].url;

    document.getElementById('dalle-image').src = url;

    //document["dalle-image"].src = url;


}