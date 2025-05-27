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


// Clear image selection
        document.getElementById('clearImage').addEventListener('click', function() {
            document.getElementById('image').value = '';
            document.getElementById('imagePreview').innerHTML = `
                <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 200px;">
                    <i class="bi bi-image text-muted" style="font-size: 3rem;"></i>
                    <span class="text-muted mt-2">Image preview will appear here</span>
                </div>`;
        });