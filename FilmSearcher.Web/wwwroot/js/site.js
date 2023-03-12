const btn = document.querySelector('.container-user-btn')
const profile = document.querySelector('.container-user-info')

btn.addEventListener('click', (e) => {
    profile.classList.add('show')
})

document.addEventListener('click', (e) => {
    const isClickInside = profile.contains(event.target) || btn.contains(event.target);

    if (!isClickInside) {
        profile.classList.remove('show')
    }
})