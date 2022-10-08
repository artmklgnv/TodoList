<template>
    <div class="form">
        <input type="text" class="input" placeholder="Description" v-model="description" />
        <button @click="createSubTask">OK</button>
        <button @click="closeWindow">Cancel</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TodoListApi from '../api/TodoListApi';
import CreateMainTask from '../Model/CreateMainTask';
import CreateSubTask from '../Model/CreateSubTask';

export default defineComponent({
    setup() {
        return {}
    },
    data() {
        var description = "";
        return {
            description
        }
    },
    props: {
        parentId: { type: Number }
    },
    methods: {
        createSubTask() {
            if (this.parentId != undefined) {
                TodoListApi.createSubTask(new CreateSubTask(this.description, this.parentId))
                    .then(res => this.updateTasks())
                    .then(res => this.closeWindow());
            }
            else {
                TodoListApi.createMainTask(new CreateMainTask(this.description))
                    .then(res => this.updateTasks())
                    .then(res => this.closeWindow());
            }
        },
        closeWindow() {
            this.$emit('close');
        },
        updateTasks() {
            this.$emit('updateTasks');
        }
    }
})
</script>

<style scoped>
.input {
    min-height: 28px;
    min-width: 300px;
}

.form {
    padding: 10px;
    margin: 10px;
    display: flex;
}

button {
    margin-left: 10px;
}
</style>