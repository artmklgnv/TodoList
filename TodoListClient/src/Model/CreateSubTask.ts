export default class CreateSubTask {
    description: string;
    parentId: number;
    constructor(desctription: string, parentId: number, ){
        this.description = desctription;
        this.parentId = parentId;
    }
}