import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, OnDestroy, OnInit, signal } from '@angular/core'
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/login.model';
import { Subscription } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { Button } from '../../../../shared/components/buttons/button/button';

@Component({
  selector: 'app-login-page',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    Button
  ],
  templateUrl: './login-page.html',
  styleUrls: ['./login-page.css','../../styles/auth.styles.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginPage implements OnDestroy, OnInit {
  private readonly authService = inject(AuthService)
  private subscription:Subscription|undefined = undefined
  private router = inject(Router)

  form = new FormGroup({
    email: new FormControl<string|undefined>(undefined,[Validators.email, Validators.required]),
    password: new FormControl<string|undefined>(undefined,[Validators.required])
  })

  isLoading = signal<boolean>(false)

  async ngOnInit(): Promise<void> {
      try {
       const isAuth = await this.authService.isAuthenticated()
       if(isAuth){
        this.router.navigate(['/products/list'])
       }
      } catch (error) {
        
      }
  }

  login(){
    if(this.form.invalid) {
      console.error('invalid form')
      return
    };

    const formValue = this.form.value

    const reques:LoginRequest = {
      email: formValue.email!,
      password: formValue.password!
    }

    this.isLoading.set(true)
    this.subscription = this.authService
    .login(reques)
    .subscribe({
      next: (data)=>{
        this.isLoading.set(false)
        this.router.navigate(['/products/list'])
      },
      error: (error) =>{
        this.isLoading.set(false)
      }
    })
  }

  ngOnDestroy(): void {
      this.subscription?.unsubscribe()
  }
}
