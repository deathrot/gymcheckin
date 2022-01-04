# Gym Checkin
This is a simple mobile application that allows ontario residents to consolidate the vaccine passport and personal ID in on app for a faster covid checkin processing at a place for business. The app is a xamarin forms built on .NET 5 using open source libraries. The app currently uses the following 3rd party packages:

- XAM.Media.Plugin
- ImageSharp
- PDFPig
- Xamarin.Forms.PinchZoomImage
- ImageCropper.Forms

 The app does not send any data anywhere and can be safely setup and used in airplane or offline mode. The app currently works with Ontario Vaccine Passports only with support coming for other provinces soon. The app does not validate the qr code or the ID of the person and leaves it to the business to verify the details. I do have plans to add QR code verification by building a fork of QRDecoderLibrary(https://github.com/StefH/QRCode) using ImageSharp instead of System.Drawing. The play and app store links will be shared soon here.

 ## ScreenShots

 - Android:








 - iOS



 ## Contribution
In order to support different provinces and countries I am looking at samples of various countries that I can test with. If you want your countries to be supported please share those images via google drive or dropbox and send me a link on my email: sharma.deepak83@gmail.com

