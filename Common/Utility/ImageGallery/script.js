const galleryItems = document.querySelectorAll('.gallery-item');

galleryItems.forEach(item => {
    item.addEventListener('click', () => {
        const src = item.src;
        const modal = document.createElement('div');
        modal.classList.add('modal');
        modal.innerHTML = `<span class="close">&times;</span><img class="modal-content" src="${src}">`;
        document.body.appendChild(modal);

        modal.querySelector('.close').onclick = () => {
            modal.remove();
        };
    });
});