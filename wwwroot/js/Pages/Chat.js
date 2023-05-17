async function textArea_valueChanged(data) {
    //console.log(data.target.getAtrribute("PromptBox"));
    const response = await fetch('http://localhost:50864/ChatWebApi/Get?data=' + data.value);

    getFormInstance().option("formData", json.data[0]);
    console.log(inputValue);
}

function getFormInstance() {
    return $("#form").dxForm("instance");
}