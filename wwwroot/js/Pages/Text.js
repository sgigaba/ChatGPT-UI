
async function ReturnAIResponse(data) {

    var option = data.value;
    var prompt = "";
    var inputText = $("#TextInputBox").dxTextArea("instance").option('value');

    if (option == "Spell Check") {
        prompt = "Please perform a spell check on the following sentance: " + inputText;
    }

    const response = await fetch('http://localhost:50864/TextWebApi/Get?data=' + prompt +"&AImodel=text-davinci-003");
    const json = await response.json();

    $("#ResponseTextBox").dxTextArea("instance").option("value", json.data[0].Message);
}