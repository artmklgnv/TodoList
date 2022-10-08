import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import { faXmark, faPlus, faPenToSquare } from '@fortawesome/free-solid-svg-icons'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'


library.add(faXmark, faPenToSquare, faPlus);
createApp(App)
    .component('font-awesome-icon', FontAwesomeIcon)
    .mount('#app');