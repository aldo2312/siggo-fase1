function UploadPhoto(params) {
    params = params || {};
    var dropbox = $(params.dropSelector),
		message = $(params.message, dropbox);
    //alert('INICIO');
        dropbox.filedrop({
        paramname: 'pic',
        clickAnchorId: 'subir',
        fakefileId: params.fileId,
        maxfiles: 5,
        maxfilesize: 2,                                                            
        url: params.url || '../../UploadImage/Index',

        uploadFinished: function (i, file, response) {
            $.data(file).addClass('done');
            var dirImg = response.path;
            $(params.valueFile).val(dirImg);
        },
        error: function (err, file) {
            switch (err) {
                case 'BrowserNotSupported':
                    showMessage('Your browser does not support HTML5 file uploads!');
                    break;
                case 'TooManyFiles':
                    alert('Too many files! Please select 5 at most! (configurable)');
                    break;
                case 'FileTooLarge':
                    alert(file.name + ' is too large! Please upload files up to 2mb (configurable).');
                    break;
                default:
                    break;
            }
        },
        beforeEach: function (file) {
            if (!file.type.match(/^image\//)) {
                alert('Only images are allowed!');

                // Returning false will cause the
                // file to be rejected
                return false;
            }
        },
        uploadStarted: function (i, file, len) {
            createImage(file);
        },
        progressUpdated: function (i, file, progress) {
            $.data(file).find('.progress').width(progress);
        }

    });
        //alert('graba');
    var template = '<div class="preview">' +
						'<span class="imageHolder">' +
							'<img style="max-height:100px;"/>' +
							'<span class="uploaded"></span>' +
						'</span>' +
						'<div class="progressHolder">' +
							'<div class="progress"></div>' +
						'</div>' +
					'</div>';


    function createImage(file) {

        var preview = $(template),
			image = $('img', preview);

        var reader = new FileReader();

        image.width = 100;
        image.height = 100;

        reader.onload = function (e) {

            // e.target.result holds the DataURL which
            // can be used as a source of the image:

            image.attr('src', e.target.result);
        };

        // Reading the file as a DataURL. When finished,
        // this will trigger the onload function above:
        reader.readAsDataURL(file);

        message.hide();
        preview.appendTo(dropbox);

        // Associating a preview container
        // with the file, using jQuery's $.data():

        $.data(file, preview);
    }

    function showMessage(msg) {
        message.html(msg);
    }

}