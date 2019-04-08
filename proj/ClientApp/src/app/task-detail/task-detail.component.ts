import { TasksService } from './../services/tasks.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskUser } from '../models/TaskUser';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {

  paramPage;
  NewOrView: string = "";
  form_disable = "";
  isNew = false;
  delete_disable = "";

  taskUser: TaskUser = {
    id: 0,
    description: "",
    lastUpdate: new Date,
    check: false,
    userId: ""
  };

  constructor(private route: ActivatedRoute,
    private taskService: TasksService,
    private router: Router) { }

  ngOnInit() {

    //check if user logged in:
    const token = localStorage.getItem("access_token");
    if (!token) {
      this.router.navigate(['home']);
    }
    else {
      this.route.params.subscribe(p => {
        this.paramPage = p['id'];

        if (Number(this.paramPage)) {
          this.NewOrView = "View Car Detail"
          this.taskService.Get(this.paramPage).subscribe(data => {
            this.taskUser = data;
            console.log("taskUser", this.taskUser);
          });
          this.form_disable = "disabled";
        }
        else if (!this.paramPage) {
          this.NewOrView = "Enter New Car";
          this.isNew = true;
        }

        this.taskUser.userId = "test";

      });
    }

  }

  onSubmit() {
    this.form_disable = "disabled";
    console.log(this.taskUser);

    if (this.isNew == true) {
      this.addNewTask();
    }
  }

  addNewTask() {
    this.taskService.CreateTask(this.taskUser).subscribe(data => {
      console.log(data);

      if (data['success'] == true) {
        setTimeout(() => {
          this.router.navigate(['/tasks']);
        }, 3000);
      }
      else {
        this.form_disable = "";
        console.log('error', data['status']);
      }

    }, err => {
      console.log('error', err);
    });
  }

  delete() {
    this.delete_disable = "disabled";
    this.taskService.DeleteTask(this.paramPage).subscribe(data => {
      console.log(data);

      if (data['success'] == true) {
        setTimeout(() => {
          this.router.navigate(['/tasks']);
        }, 3000);
      }
      else {
        this.form_disable = "";
        console.log('error', data['status']);
        this.delete_disable = "";
      }

    }, err => {
      console.log('error', err);
      this.delete_disable = "";
    });
  }



}
