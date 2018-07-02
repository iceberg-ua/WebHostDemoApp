function setValue(text) {
    editor.setContent(marked(text));
}

function getValue() {
    var opts = {};
    opts['headingStyle'] = 'atx';
    var turndownService = new TurndownService(opts);

    return turndownService.turndown(editor.getContent());
}

function setFont(name, size) {
    document.body.style.fontFamily = name;
    document.body.style.fontSize = size;
}