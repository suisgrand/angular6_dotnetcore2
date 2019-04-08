import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TaskUser } from '../models/TaskUser';
import { Observable } from 'rxjs';
import { } from ''

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  GetTasksForUser(UserId: string) {
    console.log("api", this.baseUrl + 'api/TaskUser/GetTasksForUser' + '/' + UserId);
    return this.http.get<TaskUser[]>(this.baseUrl + 'api/TaskUser/GetTasksForUser' + '/' + UserId);
  }

  Get(Id: number) {
    return this.http.get<TaskUser>(this.baseUrl + 'api/TaskUser/Get' + '/' + Id);
  }

  CreateTask(taskUser: TaskUser): Observable<TaskUser> {
    return this.http.post<TaskUser>(this.baseUrl + 'api/TaskUser/Add', taskUser);
  }

  DeleteTask(id: number): Observable<any> {
    return this.http.delete<number>(this.baseUrl + 'api/TaskUser/delete' + '/' + id);
  }


}
