import { Component } from '@angular/core';
import { MaterialModules } from '../material';

@Component({
  selector: 'app-spinner',
  standalone: true, // 👈 importante!
  imports: [MaterialModules],
  templateUrl: './spinner.html',
  styleUrls: ['./spinner.css']
})
export class Spinner {}
