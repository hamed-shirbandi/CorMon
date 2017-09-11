# ckeditor-image-uploader-plugin
An open source plugin for CKEDITOR to upload images saved on your local machine.

# How to install?

-  Click on download button and add the entire simage folder into CKEditor plugins folder.

-  Configure it with an AJAX URL for uploading the images. You can add it in your config.js as follows:
`CKEDITOR.config.extraPlugins: 'simage'`
`CKEDITOR.config.imageUploadURL: <INSERT URL>`

- You can listen to `imageUploading` event on your CKEditor instance to do anything while the image is uploading. Similarly, there is an event `imageUploaded` to perform any action after the image has successfully uploaded or the image upload failed.
