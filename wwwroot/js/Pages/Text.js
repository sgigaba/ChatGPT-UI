
async function ReturnAIResponse(data) {

    var option = data.value;
    var prompt = "";
    var inputText = $("#TextInputBox").dxTextArea("instance").option('value');

    if (option == "Spell Check") {
        prompt = "Correct the spelling on this:\n\n" + inputText;
    }
    else if (option == "Grammar Check") {
        prompt = "Correct this to standard English:\n\n" + data.value;
    }
    else if (option == "Translate into French") {
        prompt = "Translate this into French:\n\n" + data.value;
    }

    const response = await fetch('http://localhost:50864/TextWebApi/Get?data=' + prompt +"&AImodel=text-davinci-003");
    const json = await response.json();

    $("#ResponseTextBox").dxTextArea("instance").option("value", json.data[0].Message);
}