import axios from 'axios';
import CreateMainTask from '../Model/CreateMainTask';
import CreateSubTask from '../Model/CreateSubTask';
import TodoTask from '../Model/TodoTask';
import UpdateTask from '../Model/UpdateTask';

export default class RevitModelsApi {
    //static hostUrl = `https://localhost:5001`;
    static hostUrl = `https://localhost:7120`;
    constructor() {
    }

    static async getAllTasks(){
        var response = await axios.get<TodoTask[]>(this.hostUrl + `/api/TodoList`);
        var result = response.data;
        return result;
    }
    
    static async getTask(id: number){
        var response = await axios.get<TodoTask>(this.hostUrl + `/api/TodoList/` + id);
        var result = response.data;
        return result;
    }

    static deleteTask(id: number){
        var response = axios.delete(this.hostUrl + `/api/TodoList/` + id);
        return response;
    }

    static async getMainTasks(){
        var response = await axios.get<TodoTask[]>(this.hostUrl + `/api/TodoList`);
        var result = response.data;
        var filteredResult = result.filter(x=> x.parentId == null);
        return filteredResult;
    }

    static async updateTask(updateTask: UpdateTask){
        var response = await axios.put(this.hostUrl + `/api/TodoList`, updateTask);
        return response;
    }

    static async createSubTask(subTasks: CreateSubTask){
        var response = await axios.post(this.hostUrl + `/api/TodoList/addSubTask`, subTasks);
        return response;
    }

    static async createMainTask(mainTask: CreateMainTask){
        var response = await axios.post(this.hostUrl + `/api/TodoList/addMainTask`, mainTask);
        return response;
    }
}
