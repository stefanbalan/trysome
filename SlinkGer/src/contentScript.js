let qr = window.QRCode;

// testImage = (url, timeoutT) => {
//   return new Promise(function (resolve, reject) {
//     let timeout = timeoutT || 5000;
//     const img = new Image();
//     img.onerror = img.onabort = function () {
//       clearTimeout(timer);
//       reject(url);
//     };
//     img.onload = function () {
//       clearTimeout(timer);
//       resolve(url);
//     };
//     let timer = setTimeout(function () {
//       // reset .src to invalid URL so it stops previous
//       // loading, but doesn't trigger new load
//       img.src = "//!!!!/test.jpg";
//       reject(url);
//     }, timeout);
//     img.src = url;
//   });
// }

function reTestImage(url) {
  return new Promise((resolve, reject) => {
    const img = new Image();
    img.onload = () => resolve(url);
    img.onerror = () => reject(url);
    img.src = url;
  });
}

const observer = new IntersectionObserver((entries) => {
  if (!entries || !entries.length) {
    return;
  }
  entries.forEach((entry) => {
    if (entry && entry.src && entry.isIntersecting && entry.target) {
      const img = entry.target;
      const originalUrl = img.getAttribute('data-original-url');
      if (!originalUrl) {
        return;
      }
     
      console.log('reloading ' + originalUrl);
      reTestImage(originalUrl)
      .then((url) => {
        img.src = url; console.log('reloaded Ok url ' + originalUrl );
      });
    }
  });
});

// testImage('https://example.com/image.jpg')
//   .then((url) => console.log('Image loaded successfully: ' + url))
//   .catch((url) => console.error('Failed to load image: ' + url));

// for (var i = 0; i < document.images.length; i++) {
//   let image = document.images[i];
//   testImage(image.src).then(
//     (url) => console.log("ok " + url),
//     (url) =>{
//       qr.toDataURL(url)
//         .then((dataUrl) => {
//           console.log(dataUrl);
//           image.src = dataUrl;
//         })
//         .catch((err) => {
//           console.error(err);
//         });
//     });
// }

setTimeout(() => {
  for (var i = 0; i < document.images.length; i++) {
    let image = document.images[i];
    if (image && !(image.complete && image.naturalHeight !== 0)) {
      console.log('making qr for ' + image.src);
      let originalUrl = image.src;
      qr.toDataURL(image.src)
        .then((dataUrl) => {
          observer.observe(image);
          image.setAttribute('data-original-url', originalUrl);
          image.src = dataUrl;
        })
        .catch((err) => {
          console.error(err);
        });
    }
  }
}, 3000);