import { TaskUser } from './../models/TaskUser';
import { TasksService } from './../services/tasks.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  TaskUsers: TaskUser[] = [];
  constructor(private taskService: TasksService,
    private router: Router) { }

  ngOnInit() {
    const token = localStorage.getItem("access_token");
    if (!token) {
      this.router.navigate(['home']);
    }
    else {
      const user = localStorage.getItem("userId");
      this.taskService.GetTasksForUser(user).subscribe(data => {
        this.TaskUsers = data;
        console.log(this.TaskUsers);
      });
    }


  }

}
