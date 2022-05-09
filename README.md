# VueRouterHistory
[![Nuget](https://img.shields.io/nuget/v/VueRouterHistory)](https://www.nuget.org/packages/VueRouterHistory/)

用于Vue单页面应用，使用VueRouter的History模式下，通过AspNetCore提供文件服务。

# 使用
- 安装nuget包
- 在`app.UseRouting`或`app.MapControllers`之后添加`app.UseVueRouterHistory();`

```c#
 // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();
    
    // 添加这一行即可
    app.UseVueRouterHistory();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```
- 将Vue编译后的文件放到`wwwroot`目录内
- 开始体验吧。

# 遇到问题
请提交[issue](https://github.com/SpringHgui/VueRouterHistory/issues/new)给作者
