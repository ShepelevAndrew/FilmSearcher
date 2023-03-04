const config = {
    actorsData: []
}

document.addEventListener("DOMContentLoaded", () => {
    initActors()

    document.querySelector('#search').addEventListener('input', (e) => {
        const searchString = e.target.value
        search(searchString)
    })
})

const initActors = () => {
    const url = window.location.href
    const movieId = url.substring(url.lastIndexOf('/') + 1)

    fetch('/Movie/Actors?id=' + movieId, {
        method: "GET",
    }).then((resp) => {
        resp.json().then((data) => {
            config.actorsData = data
            renderSelectedActors()
        })
    })
}

const search = (searchString) => {
    fetch('/Actor/Search?search=' + searchString, {
        method: "GET",
    }).then((resp) => {
        resp.json().then((data) => {
            renderSearchResults(data)
            initSearchItemsEvents(data)
        })
    })
}

const renderSelectedActors = () => {
    const actorsContainer = document.querySelector('.list-actors')

    let html = ''
    for (const actor of config.actorsData) {
        html += `
                   <div class="selected-item" data-id="${actor.actorId}">
                       <span>${actor.fullName}</span>
                       <div class="delete-btn">
                           <img src="../../../img/icon/delete.png" />
                       </div>
                   </div>
                   `
    }

    actorsContainer.innerHTML = html
    initSelectedItemsEvents()
}

const renderSearchResults = (searchResults) => {
    if (!searchResults || !searchResults.length) {
        searchResults = []
    }

    let html = ''
    for (const actor of searchResults) {
        if (config.actorsData.find((currActor) => currActor.actorId == actor.actorId)) {
            continue
        }
        
        html += `
                           <div class="search-item" data-id="${actor.actorId}">
                               <img src="${actor.profilePictureURL}" />
                               <span>${actor.fullName}</span>
                           </div>        
                           `
    }
    document.querySelector('.list-searcher').innerHTML = html
}

const initSearchItemsEvents = (searchResults) => {
    document.querySelectorAll('.search-item').forEach((item) => {
        item.addEventListener('mouseover', (e) => {
            document.querySelector('#search').value = item.innerText
        })

        item.addEventListener('click', (e) => {
            for (const actor of searchResults) {
                if (actor.actorId == item.dataset.id) {
                    config.actorsData.push(actor)

                    renderSelectedActors()

                    document.querySelector('#search').value = ''
                    document.querySelector('.list-searcher').innerHTML = ''

                    break
                }
            }
        })
    })
}

const initSelectedItemsEvents = () => {
    document.querySelectorAll('.selected-item').forEach((item) => {
        item.addEventListener('click', (e) => {
            const actorName = item.innerText

            for (let i = 0; i < config.actorsData.length; i++) {
                if (config.actorsData[i].fullName === actorName) {
                    config.actorsData.splice(i, 1)
                }
            }

            renderSelectedActors()
        })
    })
}