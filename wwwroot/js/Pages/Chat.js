﻿async function textArea_valueChanged(data) {
    const response = await fetch('http://localhost:50864/ChatWebApi/Get?data=' + data.value);
    const json = await response.json();

    getFormInstance().option("formData", json.data[0]);
}

function getFormInstance() {
    return $("#form").dxForm("instance");
}