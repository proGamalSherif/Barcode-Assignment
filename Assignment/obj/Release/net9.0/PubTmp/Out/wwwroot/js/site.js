const imgSelectObj = document.getElementById('image')
imgSelectObj?.addEventListener('change', function (e) {
    const preview = document.querySelector('.image-preview');
    const file = e.target.files[0];

    if (file) {
        const reader = new FileReader();

        reader.onload = function (e) {
            preview.innerHTML = `<img src="${e.target.result}" alt="Preview" class="mt-2" style="width:100%;height:150px;">`;
        }

        reader.readAsDataURL(file);
    }
});