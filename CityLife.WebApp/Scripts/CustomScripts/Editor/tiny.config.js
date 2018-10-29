var tiny = function (selector, uploadUrl) {
    tinymce.init({
        selector: selector,
        height: 500,
        theme: 'modern',        
        directionality: 'ltr',
        language_url: '/Scripts/tinymce/langs/ru.js',
        plugins: "image",
        menubar: "insert",
        toolbar: "image",
        images_upload_url: uploadUrl,
        file_picker_types: 'image',
        relative_urls: false,
        remove_script_host: false,
        convert_urls: true,
        block_formats: 'Paragraph=p;Header 1=h1;Header 2=h2;Header 3=h3',
        alignleft: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'left' },
        aligncenter: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'center' },
        alignright: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'right' },
        alignjustify: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'full' },
        convert_fonts_to_spans: false,
        apply_source_formatting: true,
    plugins: [
            'advlist autolink lists link image charmap print preview hr anchor pagebreak',
            'searchreplace wordcount visualblocks visualchars code fullscreen',
            'insertdatetime media nonbreaking save table contextmenu directionality',
            'emoticons template paste textcolor colorpicker textpattern imagetools codesample toc help'
        ],
    toolbar1: 'undo redo | insert | styleselect | fontsizeselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
        toolbar2: 'forecolor backcolor | fullscreen  |help| source code',
        fontsize_formats: '8pt 10pt 12pt 14pt 18pt 24pt 36pt',
        image_advtab: true,
        
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css'
        ],
        toolbar: 'fontselect',
        font_formats: 'Arial=arial,helvetica,sans-serif;Courier New=courier new,courier,monospace;AkrutiKndPadmini=Akpdmi-n',

        file_picker_callback: function (cb, value, meta) {
            var input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');


            input.onchange = function () {
                var file = this.files[0];

                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {

                    var id = 'blobid' + (new Date()).getTime();
                    var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                    var base64 = reader.result.split(',')[1];
                    var blobInfo = blobCache.create(id, file, base64);
                    blobCache.add(blobInfo);
                    cb(blobInfo.blobUri(), { title: file.name });
                };
            };

            input.click();
        },

        //images_upload_handler: function (blobInfo, success, failure) {

        //    var xhr, formData;
        //    xhr = new XMLHttpRequest();
        //    xhr.withCredentials = false;
        //    xhr.open('POST', '/widgets/editorupload', true);
        //    xhr.onload = function () {
        //        var json;
        //        if (xhr.status != 200) {
        //            failure('HTTP error: ' + xhr.status);
        //            return;
        //        }

        //        json = JSON.parse(xhr.responseText);

        //        if (!json || typeof json.location != 'string') {
        //            failure('Invalid response ' + xhr.responseText);
        //            return;
        //        }

        //        success(json.location);
        //    }
        //}

    });
}

