export default class TodoTask {
    id: number;
    description: string;
    isDone: boolean;
    constructor(id: number, desctription: string, isDone: boolean){
        this.description = desctription;
        this.id = id;
        this.isDone = isDone;
    }
}