var inputImageChangeHandler = function (input, thumb) {
    var imgType = "/^image\//";
    var reader;
    var i = input.files.length;
    $('#rs-span').text('Выбрано фотографий: ' + i);
    for (i = 0; i < input.files.length; i++) {
        //reader = new FileReader();
        //var file = input.files[i];
        //div = document.createElement("div");
        //div.className = "ui-uploader-miniatures";

        //img = document.createElement("img");
        //img.className = "ui-uploader-thumb";
        //img.alt = "image #" + i;
        //img.file = file;
        //if (img.width < img.height) {
        //    img.height /= 3;
        //}
        //div.appendChild(img);
        //thumb.appendChild(div);
        //reader.onload = (function (a) {
        //    return function (e) { a.src = e.target.result; }
        //})(img);

        //reader.readAsDataURL(file);
        
        
    }
}

var clearThumbs = function (node) {
    for (var i = 0; i < node.childNodes.length; i++) {
        old = node.childNodes.item(i);
        node.removeChild(old);
    }
}