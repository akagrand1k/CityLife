
var inputImageChangeHandler = function (input, thumb) {
    //Указывается формат MIME типов см по ссылке
    //https://developer.mozilla.org/ru/docs/Web/HTTP/Basics_of_HTTP/MIME_types/%D0%9F%D0%BE%D0%BB%D0%BD%D1%8B%D0%B9_%D1%81%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D1%82%D0%B8%D0%BF%D0%BE%D0%B2_MIME
    var imgType = "/^image\//";
    //создаем переменную в которую прокидывается FileReader.
    var reader;

    for (var i = 0; i < input.files.length; i++) {
        reader = new FileReader();
        var file = input.files[i];
        div = document.createElement("div"); 
        div.className = "ui-uploader-miniatures";
        
        img = document.createElement("img");
        img.className = "ui-uploader-thumb";
        img.alt = "image #" + i;
        img.file = file;
        if (img.width < img.height) {
            img.height /= 3;//размер изображения, говноалгоритм. 
        }
        div.appendChild(img);
        thumb.appendChild(div);
        reader.onload = (function (a) {
            return function (e) { a.src = e.target.result; }
        })(img);

        reader.readAsDataURL(file);
    }
}

var clearThumbs = function (node) {
    for (var i = 0; i < node.childNodes.length; i++) {
        old = node.childNodes.item(i);
        node.removeChild(old);
    }
}